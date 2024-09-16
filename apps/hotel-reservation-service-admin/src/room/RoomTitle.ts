import { Room as TRoom } from "../api/room/Room";

export const ROOM_TITLE_FIELD = "roomType";

export const RoomTitle = (record: TRoom): string => {
  return record.roomType?.toString() || String(record.id);
};
