import { AbstractControl, ValidationErrors } from "@angular/forms";

class RegexConstants {
  public static password = /^[\S]{8,30}$/;
  public static username = /^[A-za-z\p{L}]{1}[A-za-z\p{L} -]{1,28}[A-za-z\p{L}]{1}$/u;
}

export class CustomValidators {

  static passwordsMatching(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;

    if ((password === confirmPassword) && (password !== null && confirmPassword !== null)) {
      return null;
    } else {
      return { passwordsNotMatching: true };
    }
  }
  static passwordMatchingRegEx(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password')?.value;
    const regEx = RegexConstants.password;
    if (regEx.test(password)) {
      return null;
    } else {
      return { passwordNotMatchingRegEx: true };
    }
  }

  static nameMatchingRegEx(control: AbstractControl): ValidationErrors | null {
    const name: string = control.get('name')?.value;
    const regEx = RegexConstants.username;
    if (regEx.test(name) 
        && !name?.includes('  ')
        && !name?.includes('--')
        && !name?.includes(' -')
        && !name?.includes('- ')
        ) {
      return null;
    } else {
      return { nameNotMatchingRegEx: true };
    }
  }

  static gradeMatching(control: AbstractControl): ValidationErrors | null {
    const grade = control.get('grade')?.value;

    if (grade >= 4 && grade <= 12 ) {
      return null;
    } else {
      return { gradeNotMatching: true };
    }
  }
}