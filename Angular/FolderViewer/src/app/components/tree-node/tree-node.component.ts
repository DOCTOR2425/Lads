import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { DirectoryTree } from '../../interfaces/directory-tree.interface';
import { FolderService } from '../../services/folder/folder.service';

@Component({
  selector: 'app-tree-node',
  imports: [CommonModule],
  templateUrl: './tree-node.component.html',
  styleUrl: './tree-node.component.scss',
})
export class TreeNodeComponent {
  @Input() nodeTree!: DirectoryTree;

  constructor(private folderService: FolderService) {}

  public selectFolder(folder: string, event: Event) {
    this.folderService.selectedFolder.next(folder);
  }
}
