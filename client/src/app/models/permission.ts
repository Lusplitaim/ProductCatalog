import { AppArea } from "./appArea";
import { AreaAction } from "./areaAction";

export interface Permission {
    area: AppArea;
    action: AreaAction;
}