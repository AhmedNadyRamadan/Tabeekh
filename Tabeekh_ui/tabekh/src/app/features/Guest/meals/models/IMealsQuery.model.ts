import { IPaginationQuery } from "../../../../core/models/IPagination.model";
import IMealFilter from "./IMealFilter.model";

type IMealsQuery = IMealFilter & IPaginationQuery;
export default IMealsQuery;