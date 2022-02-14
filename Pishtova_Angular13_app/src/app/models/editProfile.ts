import { SchoolModel } from "./school";

export interface EditProfileModel {
    school: SchoolModel;
    grade: number;
    email: string;
    name: string;
}