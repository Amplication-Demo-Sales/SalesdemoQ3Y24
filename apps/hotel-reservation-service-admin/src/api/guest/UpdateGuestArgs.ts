import { GuestWhereUniqueInput } from "./GuestWhereUniqueInput";
import { GuestUpdateInput } from "./GuestUpdateInput";

export type UpdateGuestArgs = {
  where: GuestWhereUniqueInput;
  data: GuestUpdateInput;
};
