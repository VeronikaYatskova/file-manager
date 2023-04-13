import React from 'react'

import './ErrorContainer.css';

export const ErrorContainer = (props) => {
    const {errorMessage} = props;
    return (
        <div className='error-container'>
            <span>{errorMessage}</span>
        </div>
    )
}