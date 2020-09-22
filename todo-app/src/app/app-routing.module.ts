import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import './pages/todos/todos.module';


const routes: Routes = [
  { path: 'todos', loadChildren: './pages/todos/todos.module#TodosModule'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
