export interface UserForRegistrationModel {
    name: string;
    email: string;
    password: string;
    confirmPassword: string;
    grade: number;
    schoolId: number;
    clientURI: string;
}
