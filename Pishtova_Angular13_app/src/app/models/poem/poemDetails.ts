import { PoemBaseModel } from "./poemBase";

export interface PoemDetailsModel extends PoemBaseModel{
    textUrl: string;
    analysisUrl: string;
    extrasUrl: string;
}