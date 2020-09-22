import { Component, OnInit } from '@angular/core';
import { error } from 'protractor';

import { Todo } from '../shared/todo-model';
import { TodoService } from '../shared/todo.service';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css'],
})
export class TodoListComponent implements OnInit {
  todos: Todo[] = [];

  constructor(private todoService: TodoService) {}

  ngOnInit(): void {
    this.todoService.getAll().subscribe(
      (todos) => (this.todos = todos),
      () => alert('Erro ao carregar a lista')
    );
  }

  deleteTodo(todo) {
    const mustDelete = confirm('Deseja realmente excluir este item?');
    if (mustDelete) {
      this.todoService.delete(todo.id).subscribe(
        () => (this.todos = this.todos.filter((el) => el != todo)),
        () => alert('Erro ao tentar excluir')
      );
    }
  }

  checkTodo(todo) {
    todo.isComplete = !todo.isComplete;
    this.todoService.update(todo).subscribe(
      () => null,
      () => alert('Erro ao tentar excluir')
    );
  }
}
