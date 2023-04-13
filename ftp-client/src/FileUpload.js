import React, { useState, useEffect } from "react";
import axios from "axios";

import FileList from "./components/FileList";
import "./FileUpload.css";
import { ErrorContainer } from "./components/ErrorContainer/ErrorContainer";

export const FileUpload = () => {
    const [file, setFile] = useState();
    const [fileName, setFileName] = useState();
    const [files, setFiles] = useState([]);
    const [error, setError] = useState();

    useEffect(() => {
        const getAllFiles = async () => {
          const result = await retrieveFiles();
          const allFiles = result.data;
          console.log(allFiles);
          if (allFiles) setFiles(allFiles);
        };
    
        getAllFiles();
    }, []);

    useEffect(() => {
    }, [files]);

    const retrieveFiles = async () => {
        const response = await axios.get("https://localhost:7159/api/file-manager/files", (req, res) =>{
        res.header('Access-Control-Allow-Origin', 'https://localhost:7159')
        res.end()
        }).catch(function (reqError) {
            if (reqError.response) {
                console.log(reqError.response.status);
                setError(reqError.response.status);
            }
        })

        return response;
    };

    const saveFile = (e) => {
        const file = e.target.files[0];
        console.log(file);
        setFile(file);
        setFileName(file.name);
    }

    const uploadFile = async (e) => {
        
        console.log(file);
        
        const formData = new FormData();
        
        formData.append("formFile", file);
        formData.append("fileName", fileName);
        
        try {
            const res = await axios.post("https://localhost:7159/api/file-manager/files", formData);
            setFiles([...files, res.data])
            setFileName('');
            console.log(res);
        }
        catch (ex) {
            console.log(ex);
        }
    }

    return (
        <>
            <div className='container'>
                <span className='title'>Файловый менеджер</span>
                <label>
                    <input className='input' name='file' type="file" onChange={saveFile}/>
                    <div className='button'>
                        <span className='buttonText'>Выбрать файл</span>
                    </div>
                </label>
                <button className='button' onClick={uploadFile}>Загрузить файл</button>
            </div>
            <div className='chosenFileContainer'>
                <span>Выбранный файл:   {fileName}</span>
            </div>
            {
                error === 404 ? <ErrorContainer errorMessage="Хранилище пустое" />
                              :
                                <div className='filesContainer'>
                                    <FileList files={files}/>                 
                                </div>
            }
        </>
    );
};
