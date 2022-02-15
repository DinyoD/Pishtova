import { ProfileBaseModel } from "./profileBase";
import { SchoolForProfileModel } from "../school/schoolForProfile";

export interface EditProfileModel extends ProfileBaseModel {
    school: SchoolForProfileModel;
}

