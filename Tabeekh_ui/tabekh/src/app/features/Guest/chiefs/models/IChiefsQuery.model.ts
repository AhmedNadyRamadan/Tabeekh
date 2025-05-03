import { IPaginationQuery } from "../../../../core/models/IPagination.model";
import IChiefFilter from "./IChiefFilter.model";

type IChiefsQuery = Partial<IChiefFilter> & IPaginationQuery;
export default IChiefsQuery;