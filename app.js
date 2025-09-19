// Wait for the HTML document to be fully loaded before running the script
document.addEventListener('DOMContentLoaded', () => {

    // Get references to the main elements from the HTML
    const form = document.getElementById('resume-form');
    const preview = document.getElementById('resume-preview');
    const templateSelect = document.getElementById('template-select');
    
    /**
     * A reusable function to add a new entry (like a new job or school) to a section.
     * @param {string} containerId - The ID of the container where the new entry will be added.
     * @param {Array} fields - An array of objects, each describing an input field to create.
     */
    const addEntry = (containerId, fields) => {
        const container = document.getElementById(containerId);
        const entry = document.createElement('div');
        entry.classList.add('entry'); // Add a class for styling
    
        // Create each input field based on the 'fields' array
        fields.forEach(field => {
            const input = document.createElement('input');
            input.type = field.type;
            input.placeholder = field.placeholder;
            if (field.type === 'range') {
                input.min = 1;
                input.max = 5;
                input.value = 3;
            }
            entry.appendChild(input);
        });
    
        // Create a 'Remove' button for this entry
        const removeBtn = document.createElement('button');
        removeBtn.textContent = 'Remove';
        removeBtn.addEventListener('click', () => {
            entry.remove(); // Remove the entry from the form
            updatePreview(); // Update the live preview
        });
        entry.appendChild(removeBtn);
    
        // Add the new entry to the container
        container.appendChild(entry);
    };
    
    // --- Event Listeners for 'Add' buttons ---
    
    // Add a new education entry when the 'Add Education' button is clicked
    document.getElementById('add-education').addEventListener('click', () => {
        addEntry('education-entries', [
            { type: 'text', placeholder: 'School/College' },
            { type: 'text', placeholder: 'Duration' },
            { type: 'text', placeholder: 'Description' }
        ]);
    });
    
    // Add a new work experience entry
    document.getElementById('add-work').addEventListener('click', () => {
        addEntry('work-entries', [
            { type: 'text', placeholder: 'Company/Project' },
            { type: 'text', placeholder: 'Role' },
            { type: 'text', placeholder: 'Duration' },
            { type: 'text', placeholder: 'Description' }
        ]);
    });
    
    // Add a new skill entry
    document.getElementById('add-skill').addEventListener('click', () => {
        addEntry('skills-entries', [
            { type: 'text', placeholder: 'Skill' },
            { type: 'range', placeholder: 'Proficiency' } // A slider for skill level
        ]);
    });
    
    /**
     * This is the core function that updates the live preview.
     * It reads all the data from the form, saves it to LocalStorage, and then generates the HTML for the preview.
     */
    const updatePreview = () => {
        // Create a data object to hold all the form values
        const data = {
            name: document.getElementById('name').value,
            email: document.getElementById('email').value,
            phone: document.getElementById('phone').value,
            linkedin: document.getElementById('linkedin').value,
            github: document.getElementById('github').value,
    
            // Get all education entries and map them to an array of objects
            education: Array.from(document.querySelectorAll('#education-entries .entry')).map(e => ({
                school: e.children[0].value,
                duration: e.children[1].value,
                description: e.children[2].value
            })),
    
            // Get all work entries
            work: Array.from(document.querySelectorAll('#work-entries .entry')).map(e => ({
                company: e.children[0].value,
                role: e.children[1].value,
                duration: e.children[2].value,
                description: e.children[3].value
            })),
    
            // Get all skill entries
            skills: Array.from(document.querySelectorAll('#skills-entries .entry')).map(e => ({
                skill: e.children[0].value,
                proficiency: e.children[1].value
            }))
        };
    
        // Save the entire data object to the browser's LocalStorage
        localStorage.setItem('resumeData', JSON.stringify(data));
    
        // Get the selected template ('modern' or 'classic')
        const template = templateSelect.value;
        preview.className = `resume-preview ${template}`; // Update the class for styling
    
        // Generate the HTML for the preview based on the selected template
        if (template === 'modern') {
            preview.innerHTML = `
                <div class="header">
                    <h1>${data.name || 'Your Name'}</h1>
                    <div class="contact-info">
                        ${data.email || 'your.email@example.com'} | ${data.phone || '(555) 123-4567'}
                    </div>
                    <div>
                       ${data.linkedin ? `<a href="${data.linkedin.startsWith('http') ? data.linkedin : 'https://' + data.linkedin}" target="_blank">LinkedIn</a>` : 'LinkedIn'} | 
${data.github ? `<a href="${data.github.startsWith('http') ? data.github : 'https://' + data.github}" target="_blank">GitHub</a>` : 'GitHub'}

                    </div>
                </div>
    
                <div class="section">
                    <h2>Education</h2>
                    ${data.education.map(edu => `
                        <div class="entry">
                            <strong>${edu.school || 'School Name'}</strong><br>
                            <em>${edu.duration || 'Duration'}</em><br>
                            ${edu.description || 'Description'}
                        </div>
                    `).join('')}
                </div>
    
                <div class="section">
                    <h2>Work Experience</h2>
                    ${data.work.map(w => `
                        <div class="entry">
                            <strong>${w.company || 'Company'} - ${w.role || 'Role'}</strong><br>
                            <em>${w.duration || 'Duration'}</em><br>
                            ${w.description || 'Description'}
                        </div>
                    `).join('')}
                </div>
    
                <div class="section">
                    <h2>Skills</h2>
                    ${data.skills.map(s => `
                        <div class="skill-item">
                            <span>${s.skill || 'Skill Name'}</span>
                            <span class="skill-level">${'★'.repeat(s.proficiency || 3)}${'☆'.repeat(5-(s.proficiency || 3))}</span>
                        </div>
                    `).join('')}
                </div>
            `;
        } else if (template === 'classic') {
            preview.innerHTML = `
                <div class="left-column">
                    <div class="header">
                        <h1>${data.name || 'Your Name'}</h1>
                    </div>
                    <div class="contact-info">
                        ${data.email || 'your.email@example.com'}<br>
                        ${data.phone || '(555) 123-4567'}<br>
                        ${data.linkedin ? `<a href="${data.linkedin}" target="_blank">LinkedIn</a>` : 'LinkedIn'}<br>
${data.github ? `<a href="${data.github}" target="_blank">GitHub</a>` : 'GitHub'}

                    </div>
    
                    <div class="section">
                        <h2>Skills</h2>
                        ${data.skills.map(s => `
                            <div class="skill-item">
                                <span>${s.skill || 'Skill Name'}</span>
                                <span class="skill-level">${'★'.repeat(s.proficiency || 3)}</span>
                            </div>
                        `).join('')}
                    </div>
                </div>
    
                <div class="right-column">
                    <div class="section">
                        <h2>Education</h2>
                        ${data.education.map(edu => `
                            <div class="entry">
                                <strong>${edu.school || 'School Name'}</strong><br>
                                <em>${edu.duration || 'Duration'}</em><br>
                                ${edu.description || 'Description'}
                            </div>
                        `).join('')}
                    </div>
    
                    <div class="section">
                        <h2>Work Experience</h2>
                        ${data.work.map(w => `
                            <div class="entry">
                                <strong>${w.company || 'Company'} - ${w.role || 'Role'}</strong><br>
                                <em>${w.duration || 'Duration'}</em><br>
                                ${w.description || 'Description'}
                            </div>
                        `).join('')}
                    </div>
                </div>
            `;
        }
    };
    
    /**
     * Loads the saved data from LocalStorage when the page is first loaded.
     */
    const loadFromLocalStorage = () => {
        const data = JSON.parse(localStorage.getItem('resumeData'));
        if (data) {
            // Fill the personal info fields
            document.getElementById('name').value = data.name || '';
            document.getElementById('email').value = data.email || '';
            document.getElementById('phone').value = data.phone || '';
            document.getElementById('linkedin').value = data.linkedin || '';
            document.getElementById('github').value = data.github || '';
    
            // Re-create and fill the education entries
            data.education.forEach(() => document.getElementById('add-education').click());
            Array.from(document.querySelectorAll('#education-entries .entry')).forEach((e, i) => {
                if (data.education[i]) {
                    e.children[0].value = data.education[i].school || '';
                    e.children[1].value = data.education[i].duration || '';
                    e.children[2].value = data.education[i].description || '';
                }
            });
    
            // Re-create and fill the work entries
            data.work.forEach(() => document.getElementById('add-work').click());
            Array.from(document.querySelectorAll('#work-entries .entry')).forEach((e, i) => {
                if (data.work[i]) {
                    e.children[0].value = data.work[i].company || '';
                    e.children[1].value = data.work[i].role || '';
                    e.children[2].value = data.work[i].duration || '';
                    e.children[3].value = data.work[i].description || '';
                }
            });
    
            // Re-create and fill the skill entries
            data.skills.forEach(() => document.getElementById('add-skill').click());
            Array.from(document.querySelectorAll('#skills-entries .entry')).forEach((e, i) => {
                if (data.skills[i]) {
                    e.children[0].value = data.skills[i].skill || '';
                    e.children[1].value = data.skills[i].proficiency || 3;
                }
            });
    
            // Update the preview with the loaded data
            updatePreview();
        }
    };
    
    // --- Final Event Listeners ---
    
    // Update the preview whenever any input in the form changes
    form.addEventListener('input', updatePreview);
    
    // Update the preview when the template is changed
    templateSelect.addEventListener('change', updatePreview);
    
    // FIXED: Export to PDF with proper preview update
    document.getElementById('export-pdf').addEventListener('click', () => {
        updatePreview(); // Refresh preview content before printing
        setTimeout(() => {
            window.print();
        }, 100); // Small delay to allow DOM rendering
    });
    
    // Load any saved data from LocalStorage when the page loads
    loadFromLocalStorage();
    
    // Initial preview update
    updatePreview();
    
    });
    