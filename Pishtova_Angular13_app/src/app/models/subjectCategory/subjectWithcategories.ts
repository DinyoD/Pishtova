import { SubjectInfo } from "../subject/subjectInfo";
import { CategoryWithPointsModel } from "./categoryPoints";

export interface SubjectWithCategories extends SubjectInfo {
    categories: CategoryWithPointsModel[];
}
