import React, {useState} from 'react';
import DatePicker from 'react-datepicker';
import { addDays } from 'date-fns';
import 'react-datepicker/dist/react-datepicker.css';
import './DatePicker.modules.css'

function ReactDatePicker(){
  const [startDate, setStartDate] = useState(new Date());
  const [endDate, setEndDate] = useState(new Date());
  return (
    <div className='datepicker-wrapper'>
      <DatePicker
        dateFormat="yyyy-MM-dd"
        selected={startDate}
        onChange={(date) => setStartDate(date)}
        selectsStart
        startDate={startDate}
        endDate={endDate}
      />
      <h1>-</h1>
      <DatePicker
        dateFormat="yyyy-MM-dd"
        selected={endDate}
        onChange={(date) => setEndDate(date)}
        selectsEnd
        startDate={startDate}
        endDate={endDate}
        minDate={startDate}
        maxDate={addDays(new Date(startDate), 5)}
      />
    </div>
  );
}
export default ReactDatePicker