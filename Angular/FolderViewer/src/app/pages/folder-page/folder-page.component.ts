import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FolderService } from '../../services/folder/folder.service';
import { DirectoryTree } from '../../interfaces/directory-tree.interface';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TreeNodeComponent } from '../../components/tree-node/tree-node.component';
import { bufferWhen, fromEvent } from 'rxjs';
import { DirectoryTreeComponent } from '../../components/directory-tree/directory-tree.component';
import { ExplorerComponent } from '../../components/explorer/explorer.component';

@Component({
  selector: 'app-folder-page',
  imports: [
    CommonModule,
    FormsModule,
    TreeNodeComponent,
    DirectoryTreeComponent,
    ExplorerComponent,
  ],
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

  private processResponse(response: DirectoryTree): void {
    this.directory = response;
    // this.selectedObject = this.directory.children![0].name;
    this.selectedObject = '';
    response.name = 'FolderToView';
    this.structure = JSON.stringify(response, null, 2);
  }

  public ngOnInit(): void {
    this.folderService.getDirectory().subscribe({
      next: (val) => {
        this.processResponse(val);

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
      error: (error) => {
        alert(error.error.error);
      },
    });
  }

  public create() {
    for (let char of '\\/:*?"<>|+') {
      if (this.inputName.includes(char)) {
        alert('Недопустимый символ ' + char);
        return;
      }
    }

    this.folderService
      .create(
        this.selectedObjectType,
        this.selectedObject + '\\' + this.inputName
      )
      .subscribe({
        next: (val) => {
          this.processResponse(val);
        },
        error: (error) => {
          alert(error.error.error);
        },
      });
  }

  public deleteFolder(): void {
    this.folderService.delete(this.selectedObject).subscribe({
      next: (val) => {
        this.processResponse(val);
      },
    });
  }

  public changeName(): void {
    for (let char of '\\/:*?"<>|+') {
      if (this.newName.includes(char)) {
        alert('Недопустимый символ ' + char);
        return;
      }
    }
    this.folderService.changeName(this.selectedObject, this.newName).subscribe({
      next: (val) => {
        this.processResponse(val);
        this.newName = '';
      },
      error: (error) => {
        alert(error.error.error);
      },
    });
  }

  public createFromText(): void {
    this.folderService.createFromText(this.structure).subscribe({
      next: (val) => {
        this.processResponse(val);
      },
      error: (error) => {
        alert(error.error.error);
      },
    });
  }
}
