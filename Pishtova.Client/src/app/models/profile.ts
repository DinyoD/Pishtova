import { SubjectModel } from "./subject";

export interface ProfileModel {
    id: string;
    email: string,
    pictureUrl: SVGStringList;
    name: string;
    townName: string;
    schoolName: string;
    grade: number;
    scores: UserScoreModel[];
}

interface UserScoreModel{
    pionts: number;
    subjectCategory: SubjectCategoryModel;
}

interface SubjectCategoryModel{
    id: number;
    name: string;
    subject: SubjectModel; 
}

