import * as React from "react";

import {
  Edit,
  SimpleForm,
  EditProps,
  DateTimeInput,
  ReferenceInput,
  SelectInput,
  NumberInput,
} from "react-admin";

import { GuestTitle } from "../guest/GuestTitle";
import { RoomTitle } from "../room/RoomTitle";

export const BookingEdit = (props: EditProps): React.ReactElement => {
  return (
    <Edit {...props}>
      <SimpleForm>
        <DateTimeInput label="CheckInDate" source="checkInDate" />
        <DateTimeInput label="CheckOutDate" source="checkOutDate" />
        <ReferenceInput source="guest.id" reference="Guest" label="Guest">
          <SelectInput optionText={GuestTitle} />
        </ReferenceInput>
        <ReferenceInput source="room.id" reference="Room" label="Room">
          <SelectInput optionText={RoomTitle} />
        </ReferenceInput>
        <NumberInput label="TotalPrice" source="totalPrice" />
      </SimpleForm>
    </Edit>
  );
};
