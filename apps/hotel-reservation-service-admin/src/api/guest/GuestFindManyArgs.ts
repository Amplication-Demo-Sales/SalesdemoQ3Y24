import { GuestWhereInput } from "./GuestWhereInput";
import { GuestOrderByInput } from "./GuestOrderByInput";

export type GuestFindManyArgs = {
  where?: GuestWhereInput;
  orderBy?: Array<GuestOrderByInput>;
  skip?: number;
  take?: number;
};
