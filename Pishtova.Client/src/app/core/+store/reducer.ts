import { StateModel } from './model';
import * as StateActions from './actions';

const initialState: StateModel = {
    inTest: false,
    subjectName: null
}

export function reducer(state: StateModel = initialState, action: StateActions.Actions): StateModel {
    switch(action.type) {
        case StateActions.SET_SUBJECT:
            return {...state, subjectName: action.payload };
        case StateActions.SET_IS_IN_TEST:
            return {...state, inTest: action.payload };
        default:
            return state;
    }
}