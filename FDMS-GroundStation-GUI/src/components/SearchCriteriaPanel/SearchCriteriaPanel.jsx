// NAME          : src/components/SearchCriteriaPanel/SearchCriteriaPanel.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-26
// DESCRIPTION   : Search form panel for filtering flight data

import { useGlobalContext } from '../../context/GlobalContext';
import searchFlights from '../../services/FlightSearchService';
import ToastService from '../../services/ToastService';

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

    // Show loading toast
    const toastId = ToastService.loading('Searching Database...');

    try {
      const data = await searchFlights(tailNumber, start, end);
      console.log('Search results:', data);
      setSearchResult(data);
      setIsSearchMode(true);
      setIsRealTime(false);
      ToastService.dismiss(toastId);
    } catch (error) {
      console.error('Failed to search flights:', error);
      ToastService.dismiss(toastId);
      ToastService.error('Failed to search flights');
    }
  };

  return (
    <form className="p-3 w-100 bg-panel-triary" >
      <div className="mb-0">
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
      <div className="mb-3">
        <span className="fs-6">* Leave empty to receive all flights</span>
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
