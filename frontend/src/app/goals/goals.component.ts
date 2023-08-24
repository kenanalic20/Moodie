import { Component } from '@angular/core';

@Component({
  selector: 'app-goals',
  templateUrl: './goals.component.html',
})
export class GoalsComponent {

  deleteEntry($event: any) {
    // delete the parent of the parent of the button clicked
    const parent = $event.target.parentElement;
    parent.remove();
  }

  addGoal($event: any) {
  // get #goals-holder div from parent, then add a new goal to it
  const parent = $event.target.parentElement;
  const goalsHolder = parent.querySelector('#goals-holder');
    // create div
    const element = `
    <div class="flex items-center mb-2 inline-flex justify-between">
    <div class="inline-flex">
      <input type="checkbox" class="mr-2" checked>
      <p>${
        $event.target.value
      }</p>
    </div>
    <p class="cursor-pointer" (click)="deleteEntry($event)">X</p>
  </div>
  `

    // add div to #goals-holder
    goalsHolder.innerHTML += element;

    // clear input
    $event.target.value = '';
  }}
