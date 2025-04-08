import { Component, Input } from '@angular/core';
import { DirectoryTree } from '../../interfaces/directory-tree.interface';
import { CommonModule } from '@angular/common';
import { FolderService } from '../../services/folder/folder.service';

@Component({
  selector: 'app-explorer',
  imports: [CommonModule],
  templateUrl: './explorer.component.html',
  styleUrl: './explorer.component.scss',
})
export class ExplorerComponent {
  @Input() directory!: DirectoryTree;
  public currentPath: DirectoryTree[] = [];
  public currentChildren: DirectoryTree[] = [];

  constructor(private folderService: FolderService) {}

  public ngOnInit(): void {
    if (this.directory) {
      this.navigateTo(this.directory);
    }
  }

  public navigateTo(item: DirectoryTree): void {
    if (item.children !== null) {
      this.currentPath.push(item);
      this.currentChildren = item.children || [];
      this.folderService.selectedFolder.next(this.getCurrentPathString());
    } else {
      this.folderService.selectedFolder.next(
        this.getCurrentPathString() + '\\' + item.name
      );
    }
  }

  public navigateUp(): void {
    if (this.currentPath.length > 1) {
      this.currentPath.pop();
      const parent = this.currentPath[this.currentPath.length - 1];
      this.currentChildren = parent.children || [];
      this.folderService.selectedFolder.next(this.getCurrentPathString());
    }
  }

  public navigateToRoot(): void {
    if (this.currentPath.length > 0) {
      this.currentPath = [this.currentPath[0]];
      this.currentChildren = this.currentPath[0].children || [];
      this.folderService.selectedFolder.next(this.getCurrentPathString());
    }
  }

  public getCurrentPathString(): string {
    return this.currentPath
      .slice(1)
      .map((d) => d.name)
      .join('\\');
  }

  public isDirectory(item: DirectoryTree): boolean {
    return item.children !== null;
  }

  public selectFolder(folder: string) {
    this.folderService.selectedFolder.next(folder);
  }
}
