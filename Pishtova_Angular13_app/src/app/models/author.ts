import { WorkModel } from "./work";

export interface AuthorModel {
    name: string;
    index: number;
    works: WorkModel[];
}