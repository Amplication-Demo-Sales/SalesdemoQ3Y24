import { Guest as TGuest } from "../api/guest/Guest";

export const GUEST_TITLE_FIELD = "name";

export const GuestTitle = (record: TGuest): string => {
  return record.name?.toString() || String(record.id);
};
