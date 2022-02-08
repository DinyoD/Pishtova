import { SubjectStateModel } from './model';
import * as StateActions from './actions';

const initialState: SubjectStateModel = {
    inTest: false,
    subjectName: null,
    subjectId: null,
    problemNumber:0
}

export function reducer(state: SubjectStateModel = initialState, action: StateActions.Actions): SubjectStateModel {
    switch(action.type) {
        case StateActions.SET_SUBJECT:
            return {...state, subjectName: action.payload };
        case StateActions.SET_IS_IN_TEST:
            return {...state, inTest: action.payload };
        case StateActions.SET_PROBLEM_NUMBER:
            return {...state, problemNumber: action.payload}
        default:
            return state;
    }
}