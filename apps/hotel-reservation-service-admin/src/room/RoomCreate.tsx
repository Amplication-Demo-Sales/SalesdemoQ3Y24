import * as React from "react";

import {
  Create,
  SimpleForm,
  CreateProps,
  ReferenceArrayInput,
  SelectArrayInput,
  NumberInput,
  TextInput,
} from "react-admin";

import { BookingTitle } from "../booking/BookingTitle";

export const RoomCreate = (props: CreateProps): React.ReactElement => {
  return (
    <Create {...props}>
      <SimpleForm>
        <ReferenceArrayInput source="bookings" reference="Booking">
          <SelectArrayInput
            optionText={BookingTitle}
            parse={(value: any) => value && value.map((v: any) => ({ id: v }))}
            format={(value: any) => value && value.map((v: any) => v.id)}
          />
        </ReferenceArrayInput>
        <NumberInput step={1} label="Capacity" source="capacity" />
        <TextInput label="Features" multiline source="features" />
        <TextInput label="RoomType" source="roomType" />
      </SimpleForm>
    </Create>
  );
};
