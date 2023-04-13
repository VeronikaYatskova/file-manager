import React from "react";

import "./FileList.css";

export const FileList = (props) => {
    const { files } = props;

    return (
        <>
            <div className='files-list'>
                <table>
                    <tr>
                        <th>Номер</th>
                        <th>Название</th>
                        <th></th>
                    </tr>
                    {
                        Array.isArray(files) ? files.map((f, index) => (
                            <tr>
                                <td>{index + 1}</td>
                                <td>{f.fileName}</td>
                                <td><i className='bx bxs-download' ></i></td>
                            </tr>
                        ))
                        : null
                    }
                </table>
            </div>
        </>
    );
}
