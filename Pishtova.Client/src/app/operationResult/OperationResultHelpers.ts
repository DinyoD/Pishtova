import GlobalConstants from '../common/GlobalConstants.en';
import { IVoidOperationResult } from '../operationResult/IOperationResult';

const generalErrorMessage = GlobalConstants.errorMessages.loginForm.generalLoginError;

export function setErrors(operationResult: IVoidOperationResult, errorResponse: any): void {
    const userFriendlyErrorMessage =
        errorResponse?.response?.data[GlobalConstants.userFriendlyMessageKey];
    if (userFriendlyErrorMessage) {
        operationResult.addErrorMessage(userFriendlyErrorMessage);
    } else {
        operationResult.addErrorMessage(generalErrorMessage);
    }
}

export function validateNotNull<T>(
    operationResult: IVoidOperationResult,
    data: T,
    errorMessage?: string
): void {
    if (!operationResult || data) return;
    operationResult.addErrorMessage(errorMessage || 'Unexpected null value.');
}

export function copyErrors<T extends IVoidOperationResult>(
    principal: T,
    ...secondaryResults: IVoidOperationResult[]
): T {
    if (!principal) return undefined;

    normalizeArray(secondaryResults).forEach((sr): void => {
        normalizeArray(sr.getErrorMessages()).forEach((em): void => {
            principal.addErrorMessage(em);
        });
    });

    return principal;
}

function normalizeArray<T>(array: T[]): T[] {
    if (!isValidArray(array)) return [];
    return array;
}

function isValidArray<T>(array: T[]): boolean {
    return !!array && Array.isArray(array);
}
