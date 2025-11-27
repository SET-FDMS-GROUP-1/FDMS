// NAME          : src/components/SearchCriteriaPanel/SearchCriteriaPanel.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-26
// DESCRIPTION   : Search form panel for filtering flight data

import { useGlobalContext } from '../../context/GlobalContext';
import searchFlights from '../../services/FlightSearchService';

// FUNCTION    : SearchCriteriaPanel
// DESCRIPTION : Renders a search criteria panel for filtering flight data
// PARAMETERS  : None
// RETURNS     : JSX.Element - Flight management panel component
const SearchCriteriaPanel = () => {
  const { setSearchResult, setIsSearchMode, setIsRealTime } = useGlobalContext();

  const handleSearch = async (e) => {
    e.preventDefault();
    const form = e.target.closest('form');
    const formData = new FormData(form);
    
    const tailNumber = formData.get('tailNumber');
    const startDate = formData.get('startDate');
    const startTime = formData.get('startTime');
    const endDate = formData.get('endDate');
    const endTime = formData.get('endTime');

    // Combine date and time into ISO format if both are provided
    const start = startDate && startTime ? `${startDate}T${startTime}` : startDate;
    const end = endDate && endTime ? `${endDate}T${endTime}` : endDate;

    try {
      const data = await searchFlights(tailNumber, start, end);
      console.log('Search results:', data);
      setSearchResult(data.data);
      setIsSearchMode(true);
      setIsRealTime(false);
    } catch (error) {
      console.error('Failed to search flights:', error);
      // TODO: Add user-facing error handling
    }
  };

  return (
    <form className="p-3 w-100 bg-panel-triary" >
      <div className="mb-3">
        <label htmlFor="tailNumber" className="form-label fw-semibold">
          Tail Number
        </label>
        <input
          type="text"
          className="form-control"
          id="tailNumber"
          name="tailNumber"
          placeholder="C-GCXC"
        />
      </div>

      <div className="row g-2 mb-3">
        <div className="col-6">
          <label htmlFor="startDate" className="form-label fw-semibold">
            Start Date
          </label>
          <input
            type="date"
            className="form-control"
            id="startDate"
            name="startDate"
          />
        </div>

        <div className="col-6">
          <label htmlFor="startTime" className="form-label fw-semibold">
            Start Time
          </label>
          <input
            type="time"
            className="form-control"
            id="startTime"
            name="startTime"
          />
        </div>
      </div>

      <div className="row g-2 mb-3">
        <div className="col-6">
          <label htmlFor="endDate" className="form-label fw-semibold">
            End Date
          </label>
          <input
            type="date"
            className="form-control"
            id="endDate"
            name="endDate"
          />
        </div>

        <div className="col-6">
          <label htmlFor="endTime" className="form-label1 fw-semibold mb-2">
            End Time
          </label>
          <input
            type="time"
            className="form-control"
            id="endTime"
            name="endTime"
          />
        </div>
      </div>

      <button 
        className="btn btn-primary w-100 fw-semibold" 
        onClick={handleSearch}
        type="button"
      >
        Search Database
      </button>
    </form>
  );
};

export default SearchCriteriaPanel;
