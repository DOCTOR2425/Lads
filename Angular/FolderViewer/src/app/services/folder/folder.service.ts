import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DirectoryTree } from '../../interfaces/directory-tree.interface';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FolderService {
  private baseApi: string = 'https://localhost:7204/api/Folders/';
  public selectedFolder: BehaviorSubject<string> = new BehaviorSubject('');

  constructor(private http: HttpClient) {}

  public getDirectory(): Observable<DirectoryTree> {
    return this.http.get<DirectoryTree>(`${this.baseApi}get-directory`);
  }

  public delete(path: string): Observable<DirectoryTree> {
    return this.http.delete<DirectoryTree>(`${this.baseApi}?path=${path}`);
  }

  public changeName(
    oldFullName: string,
    newName: string
  ): Observable<DirectoryTree> {
    return this.http.put<DirectoryTree>(
      `${this.baseApi}?oldName=${oldFullName}&newName=${newName}`,
      null
    );
  }

  public create(
    isFolder: boolean,
    fullName: string
  ): Observable<DirectoryTree> {
    return this.http.post<DirectoryTree>(
      `${this.baseApi}create?isFolder=${isFolder}&name=${fullName}`,
      null
    );
  }
}
