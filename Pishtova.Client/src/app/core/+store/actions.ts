import { Injectable } from '@angular/core'
import { Action } from '@ngrx/store';

export const SET_SUBJECT       = '[STORE] SetSubject';
export const SET_IS_IN_TEST    = '[STORE] SetIsInTest';

export class SetSubject implements Action {
    readonly type = SET_SUBJECT;
    constructor(public payload: string|null) {}
}

export class SetIsInTest implements Action {
    readonly type = SET_IS_IN_TEST;
    constructor(public payload: boolean) {}
}

export type Actions = SetSubject | SetIsInTest