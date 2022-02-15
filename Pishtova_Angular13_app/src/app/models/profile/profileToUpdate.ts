import { ProfileBaseModel } from "./profileBase";

export interface ProfileToUpdateModel extends ProfileBaseModel {
    schoolId: number;
}