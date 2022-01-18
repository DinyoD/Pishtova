import { reducer } from './reducer';
import { CoreState } from '../core.state';
import { ActionReducerMap } from '@ngrx/store';

export const reducers: ActionReducerMap<CoreState, any> = {
    stateModel: reducer
};