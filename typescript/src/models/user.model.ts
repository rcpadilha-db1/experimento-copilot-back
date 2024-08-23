import { User } from "src/entitys/user.entity";

export class UserModel {
    id: number;
    name: string;
    email: string;

    constructor(
        id: number = null,
        name: string = null,
        email: string = null
    ) {
        this.id = id;
        this.name = name;
        this.email = email;
    }

    toEntity(): User {
        const user = new User();
        user.id = this.id;
        user.name = this.name;
        user.email = this.email;
        return user;
    }
}