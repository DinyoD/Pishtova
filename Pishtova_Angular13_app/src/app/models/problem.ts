import { AnswerModel } from "./answer";

export interface ProblemModel{
    id: string
    pictureUrl: string;
    subjectCategoryId: number;
    questionText: string;
    hint: string;
    answers: AnswerModel[];
}