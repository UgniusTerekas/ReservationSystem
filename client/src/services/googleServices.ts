import axios from "axios";
import { GoogleEvent } from "../types/google";

const GOOGLE_CALENDER_API =
  "https://www.googleapis.com/calendar/v3/calendars/primary/events";

export async function postEventInGoogleCalendar(
  accessToken: string,
  event: GoogleEvent
) {
  axios.defaults.headers.post["Authorization"] = `Bearer ${accessToken}`;
  console.log(accessToken);
  const response = await axios
    .post(GOOGLE_CALENDER_API, event)
    .catch((error) => {
      console.log(error);
    });
  return response?.data;
}
