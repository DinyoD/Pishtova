import { PoemBaseModel } from "./poemBase";

export interface PoemWithAuthorModel extends PoemBaseModel{
    authorName: string;
    authorPictureUrl: string;
}