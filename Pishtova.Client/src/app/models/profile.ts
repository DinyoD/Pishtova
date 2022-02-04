//import { SubjectModel } from "./subject";

export interface ProfileModel {
    id: string;
    email: string,
    name: string;
    pictureUrl: SVGStringList;
    grade: number;
    townName: string;
    schoolName: string;
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
