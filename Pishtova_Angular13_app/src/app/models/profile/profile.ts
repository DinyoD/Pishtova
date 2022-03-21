//import { SubjectModel } from "./subject";

import { SchoolForProfileModel } from "../school/schoolForProfile";
import { ProfileBaseModel } from "./profileBase";

export interface ProfileModel extends ProfileBaseModel {
    id: string;
    pictureUrl: string;
    townName: string;
    school: SchoolForProfileModel;
}


