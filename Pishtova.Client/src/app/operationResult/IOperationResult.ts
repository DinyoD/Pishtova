export interface IVoidOperationResult {
    isSuccessful(): boolean;
    addErrorMessage(errorMessage: string): void;
    addErrorMessages(errorMessages: string[]): void;
    getErrorMessages(): string[];
}

export interface IOperationResult<T> extends IVoidOperationResult {
    getData(): T;
}
