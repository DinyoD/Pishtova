import { IOperationResult, IVoidOperationResult } from './IOperationResult';

export class VoidOperationResult implements IVoidOperationResult {
    private readonly _errorMessages: string[] = [];

    public addErrorMessage(errorMessage: string): boolean {
        if (!errorMessage) return false;
        this._errorMessages.push(errorMessage);
        return true;
    }

    public addErrorMessages(errorMessages: string[]): boolean {
        this._errorMessages.concat(errorMessages);

        return true;
    }

    public getErrorMessages(): string[] {
        return this._errorMessages;
    }

    public isSuccessful(): boolean {
        return this._errorMessages.length === 0;
    }
}

export class OperationResult<T> extends VoidOperationResult implements IOperationResult<T> {
    private _data!: T;

    public setData(result: T): void {
        this._data = result;
    }

    public getData(): T {
        return this._data;
    }
}
