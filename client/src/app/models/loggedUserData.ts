import { User } from "./user";

export interface LoggedUserData {
    user: User;
    token: string;
}