import { reducer } from './reducer';
import { SubjectState } from './core.state';
import { ActionReducerMap } from '@ngrx/store';

export const reducers: ActionReducerMap<SubjectState, any> = {
    subjectStateModel: reducer
};