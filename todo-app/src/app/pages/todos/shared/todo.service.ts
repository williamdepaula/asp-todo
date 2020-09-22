import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Todo } from './todo-model';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  private apiPath = 'http://localhost:5000/api/todos';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Todo[]> {
    return this.http
      .get(this.apiPath)
      .pipe(catchError(this.handleError), map(this.jsonDataToTodos));
  }

  getById(id: number): Observable<Todo> {
    const url = `${this.apiPath}/${id}`;

    return this.http
      .get(url)
      .pipe(catchError(this.handleError), map(this.jsonDataToTodo));
  }

  create(todo: Todo): Observable<Todo> {
    return this.http
      .post(this.apiPath, todo)
      .pipe(catchError(this.handleError), map(this.jsonDataToTodo));
  }

  update(todo: Todo): Observable<Todo> {
    const url = `${this.apiPath}/${todo.id}`;

    return this.http.put(url, todo).pipe(
      catchError(this.handleError),
      map(() => todo)
    );
  }

  delete(id: number): Observable<any> {
    const url = `${this.apiPath}/${id}`;

    return this.http.delete(url).pipe(
      catchError(this.handleError),
      map(() => null)
    );
  }

  private jsonDataToTodos(jsonData: any[]): Todo[] {
    const todos: Todo[] = [];
    jsonData.forEach((el) => todos.push(el as Todo));

    return todos;
  }

  private jsonDataToTodo(jsonData: any): Todo {
    return jsonData as Todo;
  }

  private handleError(error: any): Observable<any> {
    console.log('ERRO NA REQUISIÇÂO => ', error);

    return throwError(error);
  }
}
