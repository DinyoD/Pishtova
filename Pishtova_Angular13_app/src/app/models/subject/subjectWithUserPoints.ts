import { UserPointsForSubjectModel } from "../user/userPointBySubject";

export interface SubjectWithUsersPointsModel {
    usersPointsForSubject: UserPointsForSubjectModel[];
}