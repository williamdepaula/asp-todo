import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TodosRoutingModule } from './todos-routing.module';
import { TodoListComponent } from './todo-list/todo-list.component';
import { TodoFormComponent } from './todo-form/todo-form.component';


@NgModule({
  declarations: [TodoListComponent, TodoFormComponent],
  imports: [
    CommonModule,
    TodosRoutingModule
  ]
})
export class TodosModule { }
