import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FolderService } from '../../services/folder/folder.service';
import { DirectoryTree } from '../../interfaces/directory-tree.interface';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TreeNodeComponent } from '../../components/tree-node/tree-node.component';
import { bufferWhen, fromEvent } from 'rxjs';

@Component({
  selector: 'app-folder-page',
  imports: [CommonModule, FormsModule, TreeNodeComponent],
  templateUrl: './folder-page.component.html',
  styleUrl: './folder-page.component.scss',
})
export class FolderPageComponent implements OnInit {
  public directory!: DirectoryTree;
  public selectedObject: string = '';
  public inputName: string = '';
  public newName: string = '';
  public structure: string = '';
  public selectedObjectType: boolean = true;

  @ViewChild('directoryViewer', { static: false }) directoryViewer?: ElementRef;
  constructor(private folderService: FolderService) {}

  public ngOnInit(): void {
    this.folderService.getDirectory().subscribe({
      next: (val) => {
        this.directory = val;
        this.selectedObject = this.directory.children[0].name;
        val.name = 'FolderToView';
        this.structure = JSON.stringify(val, null, 2);

        this.folderService.selectedFolder
          .pipe(
            bufferWhen(() =>
              fromEvent(this.directoryViewer!.nativeElement, 'click')
            )
          )
          .subscribe((val) => {
            this.selectedObject = '';
            for (let obj of val.reverse()) {
              this.selectedObject += obj + '\\';
            }
            this.selectedObject = this.selectedObject.replace(/\\+$/, '');
          });
      },
    });
  }

  public create() {
    this.folderService
      .create(
        this.selectedObjectType,
        this.selectedObject + '\\' + this.inputName
      )
      .subscribe({
        next: (val) => {
          this.directory = val;
          this.selectedObject = this.directory.children[0].name;
        },
      });
  }

  public deleteFolder(): void {
    this.folderService.delete(this.selectedObject).subscribe({
      next: (val) => {
        this.directory = val;
        this.selectedObject = this.directory.children[0].name;
      },
    });
  }

  public changeName(): void {
    this.folderService.changeName(this.selectedObject, this.newName).subscribe({
      next: (val) => {
        this.directory = val;
        this.selectedObject = this.directory.children[0].name;
        this.newName = '';
      },
    });
  }

  public createFromText(): void {
    this.folderService.createFromText(this.structure).subscribe({
      next: (val) => {
        this.directory = val;
        this.selectedObject = this.directory.children[0].name;
      },
    });
  }
}
