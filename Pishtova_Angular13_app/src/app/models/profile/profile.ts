//import { SubjectModel } from "./subject";

import { SchoolForRegisterModel } from "../school/schoolForRegister";
import { ProfileBaseModel } from "./profileBase";

export interface ProfileModel extends ProfileBaseModel {
    id: string;
    pictureUrl: string;
    townName: string;
    school: SchoolForRegisterModel;
    stats: ProfilePointsStats;
}

interface ProfilePointsStats {
    subjects: SubjectsWithPointsByCategory[];
}

export interface SubjectsWithPointsByCategory{
    subjectName: string;
    subjectCategories: CategoryWithPoints[];
    subjectAllPoints: number;
    subjectAllProblems: number;
}

interface CategoryWithPoints{
    categoryName: string;
    points: number;
    problemsCount: number;
}
