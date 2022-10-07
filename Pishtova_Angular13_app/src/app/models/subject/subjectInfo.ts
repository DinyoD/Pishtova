import { SubjectBaseModel } from "./subjectBase";

export interface SubjectInfo extends SubjectBaseModel {
    name: string;
    points: number;
    problems: number;
}
