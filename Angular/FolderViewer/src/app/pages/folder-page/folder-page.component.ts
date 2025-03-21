import { Component, OnInit } from '@angular/core';
import { FolderService } from '../../services/folder/folder.service';

@Component({
  selector: 'app-folder-page',
  imports: [],
  templateUrl: './folder-page.component.html',
  styleUrl: './folder-page.component.scss',
})
export class FolderPageComponent implements OnInit {
  constructor(private folderService: FolderService) {}

  public ngOnInit(): void {
    this.folderService.getDirectory().subscribe((val) => console.log(val));
  }
}
