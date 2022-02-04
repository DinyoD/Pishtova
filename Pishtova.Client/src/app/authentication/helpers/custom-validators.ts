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
      console.log(12);
      
      return null;
    } else {
      console.log(0);
      
      return { nameNotMatchingRegEx: true };
    }
  }
}