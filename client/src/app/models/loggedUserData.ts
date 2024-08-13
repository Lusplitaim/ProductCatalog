import { Permission } from "./permission";
import { User } from "./user";

export interface LoggedUserData {
    user: User;
    permissions: Permission[];
    token: string;
}