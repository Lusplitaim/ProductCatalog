import { HttpParams } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { CustomQueryParams } from "../../models/customQueryParams";

export abstract class BaseApi {
  readonly baseUrl = environment.apiUrl;
    
  addQueryParams<T extends CustomQueryParams>(data: T): HttpParams {
    let params = new HttpParams();

    const keys = Object.keys(data);
    for (const key of keys) {
      let keyVal = data[key];

      if (typeof keyVal !== "boolean" && !keyVal) continue;

      if (Array.isArray(keyVal)) {
        keyVal.forEach((el) => {
          params = params.append(key, el);
        });
        continue;
      }

      params = params.append(key, keyVal);
    }

    return params;
  }
}