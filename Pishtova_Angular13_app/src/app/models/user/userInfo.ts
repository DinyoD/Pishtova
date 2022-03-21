import { BadgesCountModel } from "../badge/badgesCount";

export interface UserInfoModel{
    name: string;
    schoolName: string;
    townName: string;
    pictureUrl: string;
    grade: number;
    badges: BadgesCountModel[];
}