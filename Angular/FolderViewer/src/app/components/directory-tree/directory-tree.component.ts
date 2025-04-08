import { Component, Input } from '@angular/core';
import { DirectoryTree } from '../../interfaces/directory-tree.interface';
import { CommonModule } from '@angular/common';
import { FolderService } from '../../services/folder/folder.service';

@Component({
  imports: [CommonModule],
  selector: 'app-directory-tree',
  templateUrl: './directory-tree.component.html',
  styleUrls: ['./directory-tree.component.scss'],
})
export class DirectoryTreeComponent {
  @Input() node!: DirectoryTree;
  @Input() level = 0;
  expanded = false;

  constructor(private folderService: FolderService) {}

  public toggleExpand(): void {
    this.expanded = !this.expanded;
  }

  public selectFolder(folder: string, event: Event) {
    this.folderService.selectedFolder.next(folder);
  }
}
